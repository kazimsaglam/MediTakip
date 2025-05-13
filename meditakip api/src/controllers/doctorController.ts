import { Request, Response } from "express";
import bcrypt from "bcrypt";
import jwt from "jsonwebtoken";
import logger from "../utils/logger";
import { getPatientList } from "../services/doctorService";

export const PatientList = async (req: Request, res: Response) => {
  try {
    const response = await getPatientList();

    res.status(200).json(response);
  } catch (error) {
    logger.error("Error:", error);
    res
      .status(500)
      .json({ message: "Bir hata oluştu." });
  }
};
