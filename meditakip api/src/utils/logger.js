"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const winston_1 = require("winston");
const dotenv_1 = __importDefault(require("dotenv"));
dotenv_1.default.config();
const logLevel = process.env.LOG_LEVEL || 'info';
const logger = (0, winston_1.createLogger)({
    level: logLevel,
    format: winston_1.format.combine(winston_1.format.colorize(), winston_1.format.timestamp(), winston_1.format.printf(({ timestamp, level, message, stack }) => {
        if (stack) {
            return `${timestamp} [${level}]: ${message}\n${stack}`;
        }
        return `${timestamp} [${level}]: ${message}`;
    })),
    transports: [
        new winston_1.transports.Console(),
        new winston_1.transports.File({ filename: 'logs/app.log' }),
    ],
});
// if (process.env.NODE_ENV === 'development') {
//   logger.add(
//     new transports.Console({
//       format: format.combine(
//         format.colorize(),
//         format.simple(),
//       ),
//     })
//   );
// }
exports.default = logger;
