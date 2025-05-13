import { sqlQuery } from "../config/db";

export const fetchPrescriptionList = async (code: string | null) => {
  const request = new sqlQuery.Request();
  if (code) {
    const result = await request
      .input("PrescriptionCode", sqlQuery.NVarChar, code)
      .query(
        "SELECT * FROM [Prescriptions] as P JOIN [PrescriptionDetails] as PD ON P.PrescriptionId = PD.PrescriptionId WHERE P.PrescriptionCode = @PrescriptionCode"
      );

    return result.recordset.length > 0 ? result.recordset : [];
  } else {
    const result = await request.query(
      "SELECT * FROM [Prescriptions] as P JOIN [PrescriptionDetails] as PD ON P.PrescriptionId = PD.PrescriptionId"
    );

    return result.recordset.length > 0 ? result.recordset : [];
  }
};

export const generatePrescriptionCode = async (): Promise<string> => {
  const today = new Date().toISOString().slice(0, 10).replace(/-/g, ""); // yyyyMMdd
  const request = new sqlQuery.Request();

  const result = await request.query(`
    SELECT COUNT(*) AS count 
    FROM Prescriptions 
    WHERE CAST(PrescriptionDate AS DATE) = CAST(GETDATE() AS DATE)
  `);

  const countToday = result.recordset[0].count + 1;
  return `REC-${today}-${countToday.toString().padStart(3, "0")}`;
};

export const createPrescription = async (
  PatientId: string,
  DoctorId: string,
  Diagnosis: string,
  Drugs: any
) => {
  const PrescriptionCode = await generatePrescriptionCode();

  const insertRequest = new sqlQuery.Request();
  const insertPrescriptionResult = await insertRequest
    .input("PrescriptionCode", sqlQuery.NVarChar, PrescriptionCode)
    .input("PatientId", sqlQuery.NVarChar, PatientId)
    .input("DoctorId", sqlQuery.NVarChar, DoctorId)
    .input("Diagnosis", sqlQuery.NVarChar, Diagnosis).query(`
      INSERT INTO [Prescriptions] 
      (PrescriptionCode, PatientId, DoctorId, Diagnosis, PrescriptionDate)
      OUTPUT INSERTED.*
      VALUES (@PrescriptionCode, @PatientId, @DoctorId, @Diagnosis, GETDATE())
    `);

  const prescription = insertPrescriptionResult.recordset[0];

  if (!prescription?.PrescriptionId) {
    throw new Error("Reçete oluşturulamadı.");
  }

  const details: any[] = [];

  for (const drug of Drugs) {
    const detailResult = await new sqlQuery.Request()
      .input("PrescriptionId", sqlQuery.Int, prescription.PrescriptionId)
      .input("DrugId", sqlQuery.Int, drug.DrugId)
      .input("Dosage", sqlQuery.NVarChar, drug.Dosage)
      .input("UsagePeriod", sqlQuery.NVarChar, drug.UsagePeriod)
      .input(
        "SpecialInstructions",
        sqlQuery.NVarChar,
        drug.SpecialInstructions || null
      )
      .input("Quantity", sqlQuery.Int, drug.Quantity).query(`
        INSERT INTO PrescriptionDetails 
        (PrescriptionId, DrugId, Dosage, UsagePeriod, SpecialInstructions, Quantity)
        OUTPUT INSERTED.*
        VALUES 
        (@PrescriptionId, @DrugId, @Dosage, @UsagePeriod, @SpecialInstructions, @Quantity)
      `);

    details.push(detailResult.recordset[0]);
  }

  return {
    ...prescription,
    details,
  };
};

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
