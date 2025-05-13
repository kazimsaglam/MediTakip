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
Object.defineProperty(exports, "__esModule", { value: true });
exports.recommmendDrugs = exports.getDrugStockList = exports.getDrugsList = void 0;
const generative_ai_1 = require("@google/generative-ai");
const drugModel_1 = require("../models/drugModel");
const getDrugsList = () => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield (0, drugModel_1.fetchDrugList)();
        return {
            success: true,
            messsage: "İlaç bilgileri başarıyla alındı.",
            data: response,
        };
    }
    catch (e) { }
});
exports.getDrugsList = getDrugsList;
const getDrugStockList = () => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const response = yield (0, drugModel_1.fetchDrugStockList)();
        return {
            success: true,
            messsage: "İlaç stok bilgileri başarıyla alındı.",
            data: response,
        };
    }
    catch (e) { }
});
exports.getDrugStockList = getDrugStockList;
const recommmendDrugs = (diagnosis, age, drugs) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const drugNames = drugs.map((d) => d.Name);
        const drugsText = `[${drugNames.map((n) => `"${n}"`).join(", ")}]`;
        const prompt = `
Teşhis: "${diagnosis}"
Hasta yaşı: ${age}

Aşağıda sadece kullanılabilir ilaçlar verilmiştir:
${drugsText}

Bu teşhis ve yaşa göre en uygun 3 ilacı öner.
❗ Yalnızca yukarıda verilen ilaçlardan seç. Başka hiçbir ilaç ismi verme.
Cevap sadece JSON listesi olarak ver: ["Parol", "A-ferin", "Augmentin"]
`;
        const genAI = new generative_ai_1.GoogleGenerativeAI("AIzaSyCDiE3ynGCpv_M9uApK2JhIFeLxZPEo5Kw");
        const model = genAI.getGenerativeModel({ model: "gemini-1.5-pro-latest" });
        const result = yield model.generateContent(prompt);
        const text = result.response.text();
        return {
            success: true,
            messsage: "Önerilen ilaç bilgileri başarıyla gönderildi.",
            data: text,
        };
    }
    catch (e) {
        console.log(e);
    }
});
exports.recommmendDrugs = recommmendDrugs;
