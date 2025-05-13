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
exports.getByUserWithInformations = void 0;
const db_1 = require("../config/db");
const getByUserWithInformations = (username, password, userType) => __awaiter(void 0, void 0, void 0, function* () {
    const request = new db_1.sqlQuery.Request();
    const result = yield request
        .input('Username', db_1.sqlQuery.NVarChar, username)
        .input('Password', db_1.sqlQuery.NVarChar, password)
        .input('UserType', db_1.sqlQuery.NVarChar, userType)
        .query('SELECT * FROM [Users] WHERE Username = @Username AND Password = @Password AND UserType = @UserType');
    return result.recordset[0] || null;
});
exports.getByUserWithInformations = getByUserWithInformations;
