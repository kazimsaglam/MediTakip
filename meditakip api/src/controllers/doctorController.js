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
exports.PatientList = void 0;
const logger_1 = __importDefault(require("../utils/logger"));
const doctorService_1 = require("../services/doctorService");
const PatientList = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield (0, doctorService_1.getPatientList)();
        res.status(200).json(response);
    }
    catch (error) {
        logger_1.default.error("Error:", error);
        res
            .status(500)
            .json({ message: "Bir hata oluştu." });
    }
});
exports.PatientList = PatientList;
