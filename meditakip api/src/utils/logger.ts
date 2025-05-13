import { createLogger, format, transports } from 'winston';
import dotenv from 'dotenv';

dotenv.config();

const logLevel = process.env.LOG_LEVEL || 'info'; 

const logger = createLogger({
  level: logLevel,
  format: format.combine(
    format.colorize(),
    format.timestamp(),
    format.printf(({ timestamp, level, message, stack }) => {
      if (stack) {
        return `${timestamp} [${level}]: ${message}\n${stack}`;
      }
      return `${timestamp} [${level}]: ${message}`;
    })
  ),
  transports: [
    new transports.Console(),
    new transports.File({ filename: 'logs/app.log' }),
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

export default logger;