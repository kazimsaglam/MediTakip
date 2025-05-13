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
exports.createPrescriptionWithDetails = exports.getPrescriptionList = void 0;
const prescriptionModel_1 = require("../models/prescriptionModel");
function groupPrescriptions(rows) {
    if (!Array.isArray(rows))
        return [];
    const prescriptionsMap = new Map();
    for (const row of rows) {
        const key = row.PrescriptionId;
        if (!prescriptionsMap.has(key)) {
            prescriptionsMap.set(key, {
                PrescriptionId: row.PrescriptionId[0],
                PrescriptionCode: row.PrescriptionCode,
                PatientId: row.PatientId,
                DoctorId: row.DoctorId,
                Diagnosis: row.Diagnosis,
                PrescriptionDate: row.PrescriptionDate,
                details: [],
            });
        }
        if (row.DetailId != null) {
            prescriptionsMap.get(key).details.push({
                DetailId: row.DetailId,
                DrugId: row.DrugId,
                Dosage: row.Dosage,
                UsagePeriod: row.UsagePeriod,
                SpecialInstructions: row.SpecialInstructions,
                Quantity: row.Quantity,
            });
        }
    }
    return Array.from(prescriptionsMap.values());
}
const getPrescriptionList = (code) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield (0, prescriptionModel_1.fetchPrescriptionList)(code);
        return {
            success: true,
            messsage: "Reçete bilgileri başarıyla alındı.",
            data: groupPrescriptions(response),
        };
    }
    catch (e) { }
});
exports.getPrescriptionList = getPrescriptionList;
const createPrescriptionWithDetails = (PatientId, DoctorId, Diagnosis, Drugs) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield (0, prescriptionModel_1.createPrescription)(PatientId, DoctorId, Diagnosis, Drugs);
        return {
            success: true,
            messsage: "Reçete bilgileri başarıyla oluşturuldu.",
            data: response,
        };
    }
    catch (e) {
        console.log(e);
    }
});
exports.createPrescriptionWithDetails = createPrescriptionWithDetails;
