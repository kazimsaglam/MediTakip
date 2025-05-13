import { fetchPrescriptionList, createPrescription } from "../models/prescriptionModel";

function groupPrescriptions(rows: any) {

  if (!Array.isArray(rows)) return [];

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

export const getPrescriptionList = async (code: string | null) => {
  try {
    const response = await fetchPrescriptionList(code);

    return {
      success: true,
      messsage: "Reçete bilgileri başarıyla alındı.",
      data: groupPrescriptions(response),
    };
  } catch (e) {}
};


export const createPrescriptionWithDetails = async (PatientId: string, DoctorId: string, Diagnosis: string, Drugs: any) => {
  try {
    const response = await createPrescription(PatientId, DoctorId, Diagnosis, Drugs);

    return {
      success: true,
      messsage: "Reçete bilgileri başarıyla oluşturuldu.",
      data: response,
    };

  } catch (e) {
    console.log(e);
  }
};
