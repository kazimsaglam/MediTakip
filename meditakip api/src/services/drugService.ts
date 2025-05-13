import { GoogleGenerativeAI } from "@google/generative-ai";
import { fetchDrugList, fetchDrugStockList } from "../models/drugModel";

export const getDrugsList = async () => {
  try {
    const response = await fetchDrugList();

    return {
      success: true,
      messsage: "İlaç bilgileri başarıyla alındı.",
      data: response,
    };
  } catch (e) {}
};

export const getDrugStockList = async () => {
  try {
    const response = await fetchDrugStockList();

    return {
      success: true,
      messsage: "İlaç stok bilgileri başarıyla alındı.",
      data: response,
    };
  } catch (e) {}
};

export const recommmendDrugs = async (
  diagnosis: string,
  age: string,
  drugs: any
) => {
  try {
    const drugNames = drugs.map((d: any) => d.Name);
    const drugsText = `[${drugNames.map((n: any) => `"${n}"`).join(", ")}]`;

    const prompt = `
Teşhis: "${diagnosis}"
Hasta yaşı: ${age}

Aşağıda sadece kullanılabilir ilaçlar verilmiştir:
${drugsText}

Bu teşhis ve yaşa göre en uygun 3 ilacı öner.
❗ Yalnızca yukarıda verilen ilaçlardan seç. Başka hiçbir ilaç ismi verme.
Cevap sadece JSON listesi olarak ver: ["Parol", "A-ferin", "Augmentin"]
`;

    const genAI = new GoogleGenerativeAI("AIzaSyCDiE3ynGCpv_M9uApK2JhIFeLxZPEo5Kw");
    const model = genAI.getGenerativeModel({ model: "gemini-1.5-pro-latest" });

    const result = await model.generateContent(prompt);
    const text = result.response.text();

    return {
      success: true,
      messsage: "Önerilen ilaç bilgileri başarıyla gönderildi.",
      data: text,
    };
  } catch (e) {
    console.log(e);
  }
};
