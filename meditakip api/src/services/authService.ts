import { getByUserWithInformations } from "../models/userModel";

export const loginAccount = async (
  username: string,
  password: string,
  userType: string
) => {
  try {
    const result = await getByUserWithInformations(
      username,
      password,
      userType
    );

    if (result) {
      return {
        success: true,
        messsage: "Başarıyla giriş yapıldı.",
        data: null,
      };
    } else {
      return {
        success: false,
        messsage: "Kullanıcı adı veya şifre hatalı olabilir.",
        data: null,
      };
    }
  } catch (e) {}
};
