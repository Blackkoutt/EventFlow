import { useOutletContext } from "react-router-dom";
import { User } from "../../models/response_models";
import ProfileInfo from "../../components/profile/ProfileInfo";
import DateFormatter from "../../helpers/DateFormatter";
import { DateFormat } from "../../helpers/enums/DateFormatEnum";

const UserInfo = () => {
  const info = useOutletContext<User>();

  return info ? (
    <article className="flex flex-col justify-start items-start p-4">
      <h3 className="font-bold text-[27px] pb-4">Podstawowe informacje o użytkowniku:</h3>
      <div className="flex flex-col justify-start items-start gap-4">
        <ProfileInfo infoTitle="Imię:" infoContent={info.name} />
        <ProfileInfo infoTitle="Nazwisko:" infoContent={info.surname} />
        <ProfileInfo infoTitle="Adres e-mail:" infoContent={info.emailAddress} />
        <ProfileInfo
          infoTitle="Data urodzenia:"
          infoContent={DateFormatter.FormatDate(info.dateOfBirth, DateFormat.Date)}
        />
        <ProfileInfo infoTitle="Role:" infoContent={info.userRoles.join(", ")} />
      </div>
    </article>
  ) : (
    ""
  );
};
export default UserInfo;
