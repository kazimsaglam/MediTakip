import sql from 'mssql';
import dotenv from 'dotenv';
import logger from '../utils/logger';

dotenv.config();

console.log(process.env);
const config: sql.config = {
    user: process.env.MSSQL_USER,
    password: process.env.MSSQL_PASSWORD,
    database: process.env.MSSQL_DATABASE,
    server: process.env.MSSQL_SERVER as string,
    port: parseFloat(process.env.MSSQL_PORT as string),
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
export const connectDB = async () => {
  try {
    await sql.connect(config);
    logger.info('Connected to MSSQL database');
  } catch (error) {
    logger.error('Connected to MSSQL database',error);
    throw new Error('Database connection failed');
  }
};

export const sqlQuery = sql;