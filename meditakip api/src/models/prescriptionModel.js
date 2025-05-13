"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.createPrescription = exports.generatePrescriptionCode = exports.fetchPrescriptionList = void 0;
const db_1 = require("../config/db");
const fetchPrescriptionList = (code) => __awaiter(void 0, void 0, void 0, function* () {
    const request = new db_1.sqlQuery.Request();
    if (code) {
        const result = yield request
            .input("PrescriptionCode", db_1.sqlQuery.NVarChar, code)
            .query("SELECT * FROM [Prescriptions] as P JOIN [PrescriptionDetails] as PD ON P.PrescriptionId = PD.PrescriptionId WHERE P.PrescriptionCode = @PrescriptionCode");
        return result.recordset.length > 0 ? result.recordset : [];
    }
    else {
        const result = yield request.query("SELECT * FROM [Prescriptions] as P JOIN [PrescriptionDetails] as PD ON P.PrescriptionId = PD.PrescriptionId");
        return result.recordset.length > 0 ? result.recordset : [];
    }
});
exports.fetchPrescriptionList = fetchPrescriptionList;
const generatePrescriptionCode = () => __awaiter(void 0, void 0, void 0, function* () {
    const today = new Date().toISOString().slice(0, 10).replace(/-/g, ""); // yyyyMMdd
    const request = new db_1.sqlQuery.Request();
    const result = yield request.query(`
    SELECT COUNT(*) AS count 
    FROM Prescriptions 
    WHERE CAST(PrescriptionDate AS DATE) = CAST(GETDATE() AS DATE)
  `);
    const countToday = result.recordset[0].count + 1;
    return `REC-${today}-${countToday.toString().padStart(3, "0")}`;
});
exports.generatePrescriptionCode = generatePrescriptionCode;
const createPrescription = (PatientId, DoctorId, Diagnosis, Drugs) => __awaiter(void 0, void 0, void 0, function* () {
    const PrescriptionCode = yield (0, exports.generatePrescriptionCode)();
    const insertRequest = new db_1.sqlQuery.Request();
    const insertPrescriptionResult = yield insertRequest
        .input("PrescriptionCode", db_1.sqlQuery.NVarChar, PrescriptionCode)
        .input("PatientId", db_1.sqlQuery.NVarChar, PatientId)
        .input("DoctorId", db_1.sqlQuery.NVarChar, DoctorId)
        .input("Diagnosis", db_1.sqlQuery.NVarChar, Diagnosis).query(`
      INSERT INTO [Prescriptions] 
      (PrescriptionCode, PatientId, DoctorId, Diagnosis, PrescriptionDate)
      OUTPUT INSERTED.*
      VALUES (@PrescriptionCode, @PatientId, @DoctorId, @Diagnosis, GETDATE())
    `);
    const prescription = insertPrescriptionResult.recordset[0];
    if (!(prescription === null || prescription === void 0 ? void 0 : prescription.PrescriptionId)) {
        throw new Error("Reçete oluşturulamadı.");
    }
    const details = [];
    for (const drug of Drugs) {
        const detailResult = yield new db_1.sqlQuery.Request()
            .input("PrescriptionId", db_1.sqlQuery.Int, prescription.PrescriptionId)
            .input("DrugId", db_1.sqlQuery.Int, drug.DrugId)
            .input("Dosage", db_1.sqlQuery.NVarChar, drug.Dosage)
            .input("UsagePeriod", db_1.sqlQuery.NVarChar, drug.UsagePeriod)
            .input("SpecialInstructions", db_1.sqlQuery.NVarChar, drug.SpecialInstructions || null)
            .input("Quantity", db_1.sqlQuery.Int, drug.Quantity).query(`
        INSERT INTO PrescriptionDetails 
        (PrescriptionId, DrugId, Dosage, UsagePeriod, SpecialInstructions, Quantity)
        OUTPUT INSERTED.*
        VALUES 
        (@PrescriptionId, @DrugId, @Dosage, @UsagePeriod, @SpecialInstructions, @Quantity)
      `);
        details.push(detailResult.recordset[0]);
    }
    return Object.assign(Object.assign({}, prescription), { details });
});
exports.createPrescription = createPrescription;
// export const fetchPrescriptionList = async (code: string | null) => {
//   const request = new sqlQuery.Request();
//   if (code) {
//     const result = await request
//       .input("PrescriptionCode", sqlQuery.NVarChar, code)
//       .query(
//         "SELECT * FROM [Prescriptions] as P  WHERE P.PrescriptionCode = @PrescriptionCode"
//       );
//     return result.recordset[0] || null;
//   } else {
//     const result = await request.query("SELECT * FROM [Prescriptions]");
//     return result.recordset.length > 0 ? result.recordset : [];
//   }
// };
// export const fetchPrescriptionDetailList = async (code: string) => {
//   const request = new sqlQuery.Request();
//   const result = await request
//     .input("PrescriptionCode", sqlQuery.NVarChar, code)
//     .query(
//       "SELECT * FROM [PrescriptionDetails] as PD JOIN [Prescriptions] as P ON P.PrescriptionId = PD.PrescriptionId WHERE P.PrescriptionCode = @PrescriptionCode"
//     );
//   return result.recordset.length > 0 ? result.recordset : [];
// };
