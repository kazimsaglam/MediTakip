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
exports.createPrescription = exports.prescriptionList = void 0;
const logger_1 = __importDefault(require("../utils/logger"));
const prescriptionService_1 = require("../services/prescriptionService");
const zod_1 = require("zod");
const prescriptionList = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const { code } = req.params;
    try {
        const response = yield (0, prescriptionService_1.getPrescriptionList)(code);
        res.status(200).json(response);
    }
    catch (error) {
        logger_1.default.error("Error:", error);
        res.status(500).json({ message: "Bir hata oluştu." });
    }
});
exports.prescriptionList = prescriptionList;
const createPrescription = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const { PatientId, DoctorId, Diagnosis, Drugs } = req.body;
    try {
        const prescriptionSchema = zod_1.z.object({
            PatientId: zod_1.z.string(),
            DoctorId: zod_1.z.string(),
            Diagnosis: zod_1.z.string(),
            Drugs: zod_1.z.array(zod_1.z.object({
                DrugId: zod_1.z.number(),
                Dosage: zod_1.z.string(),
                UsagePeriod: zod_1.z.string(),
                SpecialInstructions: zod_1.z.string() || null,
                Quantity: zod_1.z.number()
            })),
        });
        if (!PatientId ||
            !DoctorId ||
            !Diagnosis ||
            !Array.isArray(Drugs) ||
            Drugs.length === 0) {
            return res.status(400).json({
                success: false,
                message: "Parametleri lütfen doğru gönderiniz.",
                data: null,
            });
        }
        try {
            const validated = prescriptionSchema.parse(req.body);
        }
        catch (err) {
            return res.status(400).json({
                success: false,
                message: "Parametleri lütfen doğru gönderiniz.",
                data: null,
            });
        }
        const response = yield (0, prescriptionService_1.createPrescriptionWithDetails)(PatientId, DoctorId, Diagnosis, Drugs);
        res.status(200).json(response);
    }
    catch (error) {
        logger_1.default.error("Error:", error);
        res.status(500).json({ message: "Bir hata oluştu." });
    }
});
exports.createPrescription = createPrescription;
