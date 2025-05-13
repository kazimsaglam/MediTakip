import { Request, Response } from "express";
import bcrypt from "bcrypt";
import jwt from "jsonwebtoken";
import logger from "../utils/logger";
import { loginAccount } from "../services/authService";

export const login = async (req: Request, res: Response) => {
  const { username, password, userType } = req.body;
  try {
    if (!username || !password || !userType) {
      return res
        .status(400)
        .json({
          success: false,
          message: "Lütfen tüm parametleri doldurunuz.",
          data: null,
        });
    }
    const accountData = await loginAccount(username, password, userType);
    res.status(200).json(accountData);
  } catch (error) {
    logger.error("Error:", error);
    res
      .status(500)
      .json({ message: "Giriş yapılırken bir hata oluştu." });
  }
};
