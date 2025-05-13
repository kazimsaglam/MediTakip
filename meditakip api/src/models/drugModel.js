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
exports.fetchDrugStockList = exports.fetchDrugList = void 0;
const db_1 = require("../config/db");
const fetchDrugList = () => __awaiter(void 0, void 0, void 0, function* () {
    const request = new db_1.sqlQuery.Request();
    const result = yield request
        .query('SELECT * FROM [Drugs]');
    return result.recordset.length > 0 ? result.recordset : [];
});
exports.fetchDrugList = fetchDrugList;
const fetchDrugStockList = () => __awaiter(void 0, void 0, void 0, function* () {
    const request = new db_1.sqlQuery.Request();
    const result = yield request
        .query('SELECT * FROM [DrugStocks]');
    return result.recordset.length > 0 ? result.recordset : [];
});
exports.fetchDrugStockList = fetchDrugStockList;
