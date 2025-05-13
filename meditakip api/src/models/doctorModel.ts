import { sqlQuery } from '../config/db';

export const fetchPatientList = async () => {
  const request = new sqlQuery.Request();
  const result = await request
    .query('SELECT * FROM [Patients]');

  return result.recordset.length > 0 ? result.recordset : [];
};