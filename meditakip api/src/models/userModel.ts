import { sqlQuery } from '../config/db';

export const getByUserWithInformations = async (username: string, password: string, userType: string) => {
  const request = new sqlQuery.Request();
  const result = await request
    .input('Username', sqlQuery.NVarChar, username)
    .input('Password', sqlQuery.NVarChar, password)
    .input('UserType', sqlQuery.NVarChar, userType)
    .query('SELECT * FROM [Users] WHERE Username = @Username AND Password = @Password AND UserType = @UserType');

  return result.recordset[0] || null;
};