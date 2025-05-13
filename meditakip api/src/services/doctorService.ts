import { fetchPatientList } from "../models/doctorModel";

export const getPatientList = async (
) => {
  try {
    const response = await fetchPatientList();

    return {
      success: true,
      messsage: "Hasta bilgileri başarıyla alındı.",
      data: response,
    };
  } catch (e) {}
};
