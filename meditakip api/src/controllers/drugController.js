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
exports.getRecommendDrugs = exports.drugStockList = exports.drugsList = void 0;
const logger_1 = __importDefault(require("../utils/logger"));
const drugService_1 = require("../services/drugService");
const drugsList = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield (0, drugService_1.getDrugsList)();
        res.status(201).json(response);
    }
    catch (error) {
        logger_1.default.error("Error:", error);
        res.status(500).json({ message: "Bir hata oluştu." });
    }
});
exports.drugsList = drugsList;
const drugStockList = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield (0, drugService_1.getDrugStockList)();
        res.status(200).json(response);
    }
    catch (error) {
        logger_1.default.error("Error:", error);
        res.status(500).json({ message: "Bir hata oluştu." });
    }
});
exports.drugStockList = drugStockList;
const getRecommendDrugs = (req, res) => __awaiter(void 0, void 0, void 0, function* () {
    const { diagnosis, age, drugs } = req.body;
    try {
        if (!diagnosis || !age || !drugs) {
            return res.status(400).json({
                success: false,
                message: "Lütfen tüm parametleri doldurunuz.",
                data: null,
            });
        }
        const response = yield (0, drugService_1.recommmendDrugs)(diagnosis, age, drugs);
        res.status(200).json(response);
    }
    catch (error) {
        logger_1.default.error("Error:", error);
        res.status(500).json({ message: "Giriş yapılırken bir hata oluştu." });
    }
});
exports.getRecommendDrugs = getRecommendDrugs;
