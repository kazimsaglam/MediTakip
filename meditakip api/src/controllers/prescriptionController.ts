import { Request, Response } from "express";
import bcrypt from "bcrypt";
import jwt from "jsonwebtoken";
import logger from "../utils/logger";
import { getPrescriptionList, createPrescriptionWithDetails } from "../services/prescriptionService";
import { z } from "zod";

export const prescriptionList = async (req: Request, res: Response) => {
  const { code } = req.params;
  try {
    const response = await getPrescriptionList(code);

    res.status(200).json(response);
  } catch (error) {
    logger.error("Error:", error);
    res.status(500).json({ message: "Bir hata oluştu." });
  }
};

export const createPrescription = async (req: Request, res: Response) => {
  const { PatientId, DoctorId, Diagnosis, Drugs } = req.body;
  try {
    const prescriptionSchema = z.object({
      PatientId: z.string(),
      DoctorId: z.string(),
      Diagnosis: z.string(),
      Drugs: z.array(
        z.object({
          DrugId: z.number(),
          Dosage: z.string(),
          UsagePeriod: z.string(),
          SpecialInstructions: z.string() || null,
          Quantity: z.number()
        })
      ),
    });

    if (
      !PatientId ||
      !DoctorId ||
      !Diagnosis ||
      !Array.isArray(Drugs) ||
      Drugs.length === 0
    ) {
      return res.status(400).json({
        success: false,
        message: "Parametleri lütfen doğru gönderiniz.",
        data: null,
      });
    }

    try {
      const validated = prescriptionSchema.parse(req.body);
    } catch (err) {
      return res.status(400).json({
        success: false,
        message: "Parametleri lütfen doğru gönderiniz.",
        data: null,
      });
    }

    const response = await createPrescriptionWithDetails(PatientId, DoctorId, Diagnosis, Drugs);

    res.status(200).json(response);
  } catch (error) {
    logger.error("Error:", error);
    res.status(500).json({ message: "Bir hata oluştu." });
  }
};
