"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const dotenv_1 = __importDefault(require("dotenv"));
const cors_1 = __importDefault(require("cors"));
const authRoutes_1 = __importDefault(require("./routes/authRoutes"));
const doctorRoutes_1 = __importDefault(require("./routes/doctorRoutes"));
const drugRoutes_1 = __importDefault(require("./routes/drugRoutes"));
const prescriptionRoutes_1 = __importDefault(require("./routes/prescriptionRoutes"));
const db_1 = require("./config/db");
dotenv_1.default.config();
const app = (0, express_1.default)();
app.use(express_1.default.json());
//app.use(morgan('dev'));
app.use((0, cors_1.default)());
(0, db_1.connectDB)();
app.use('/api/auth/', authRoutes_1.default);
app.use('/api/doctor/', doctorRoutes_1.default);
app.use('/api/drug/', drugRoutes_1.default);
app.use('/api/prescription/', prescriptionRoutes_1.default);
app.use((req, res) => {
    res.status(404).json({ data: 'Path error' });
});
exports.default = app;
