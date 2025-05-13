import { Request, Response } from "express";
import bcrypt from "bcrypt";
import jwt from "jsonwebtoken";
import logger from "../utils/logger";
import { getDrugsList, getDrugStockList, recommmendDrugs } from "../services/drugService";

export const drugsList = async (req: Request, res: Response) => {
  try {
    const response = await getDrugsList();

    res.status(201).json(response);
  } catch (error) {
    logger.error("Error:", error);
    res.status(500).json({ message: "Bir hata oluştu." });
  }
};

export const drugStockList = async (req: Request, res: Response) => {
  try {
    const response = await getDrugStockList();

    res.status(200).json(response);
  } catch (error) {
    logger.error("Error:", error);
    res.status(500).json({ message: "Bir hata oluştu." });
  }
};

export const getRecommendDrugs = async (req: Request, res: Response) => {
  const { diagnosis, age, drugs } = req.body;
  try {
    if (!diagnosis || !age || !drugs) {
      return res.status(400).json({
        success: false,
        message: "Lütfen tüm parametleri doldurunuz.",
        data: null,
      });
    }

    const response = await recommmendDrugs(diagnosis, age, drugs);
    
    res.status(200).json(response);
  } catch (error) {
    logger.error("Error:", error);
    res.status(500).json({ message: "Giriş yapılırken bir hata oluştu." });
  }
};
