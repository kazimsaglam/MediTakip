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
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.login = void 0;
const logger_1 = __importDefault(require("../utils/logger"));
const authService_1 = require("../services/authService");
const login = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
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
        const accountData = yield (0, authService_1.loginAccount)(username, password, userType);
        res.status(200).json(accountData);
    }
    catch (error) {
        logger_1.default.error("Error:", error);
        res
            .status(500)
            .json({ message: "Giriş yapılırken bir hata oluştu." });
    }
});
exports.login = login;
