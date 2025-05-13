import { sqlQuery } from '../config/db';

export const fetchDrugList = async () => {
  const request = new sqlQuery.Request();
  const result = await request
    .query('SELECT * FROM [Drugs]');

  return result.recordset.length > 0 ? result.recordset : [];
};

export const fetchDrugStockList = async () => {
  const request = new sqlQuery.Request();
  const result = await request
    .query('SELECT * FROM [DrugStocks]');

  return result.recordset.length > 0 ? result.recordset : [];
};