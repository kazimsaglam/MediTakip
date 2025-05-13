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
exports.loginAccount = void 0;
const userModel_1 = require("../models/userModel");
const loginAccount = (username, password, userType) => __awaiter(void 0, void 0, void 0, function* () {
    try {
        const result = yield (0, userModel_1.getByUserWithInformations)(username, password, userType);
        if (result) {
            return {
                success: true,
                messsage: "Başarıyla giriş yapıldı.",
                data: null,
            };
        }
        else {
            return {
                success: false,
                messsage: "Kullanıcı adı veya şifre hatalı olabilir.",
                data: null,
            };
        }
    }
    catch (e) { }
});
exports.loginAccount = loginAccount;
