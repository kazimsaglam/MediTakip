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
exports.sqlQuery = exports.connectDB = void 0;
const mssql_1 = __importDefault(require("mssql"));
const dotenv_1 = __importDefault(require("dotenv"));
const logger_1 = __importDefault(require("../utils/logger"));
dotenv_1.default.config();
console.log(process.env);
const config = {
    user: process.env.MSSQL_USER,
    password: process.env.MSSQL_PASSWORD,
    database: process.env.MSSQL_DATABASE,
    server: process.env.MSSQL_SERVER,
    port: parseFloat(process.env.MSSQL_PORT),
    pool: {
        max: 50,
        min: 0,
        idleTimeoutMillis: 60000,
    },
    options: {
        trustServerCertificate: true,
        appName: "flight",
    },
    requestTimeout: 180000,
    connectionTimeout: 180000,
};
const connectDB = () => __awaiter(void 0, void 0, void 0, function* () {
    try {
        yield mssql_1.default.connect(config);
        logger_1.default.info('Connected to MSSQL database');
    }
    catch (error) {
        logger_1.default.error('Connected to MSSQL database', error);
        throw new Error('Database connection failed');
    }
});
exports.connectDB = connectDB;
exports.sqlQuery = mssql_1.default;
