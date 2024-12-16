import { useOutletContext } from "react-router-dom";
import { User } from "../../models/response_models";
import ProfileInfo from "../../components/profile/ProfileInfo";

const UserAdditionalInfo = () => {
  const info = useOutletContext<User>();

  return (
    <article className="flex flex-col justify-start items-start p-4">
      <h3 className="font-bold text-[27px] pb-4">Dodatkowe informacje o użytkowniku:</h3>
      <p className="flex flex-col justify-start items-start gap-4">
        <ProfileInfo infoTitle="Ulica:" infoContent={info.userData?.street} />
        <ProfileInfo infoTitle="Nr domu:" infoContent={`${info.userData?.houseNumber}`} />
        <ProfileInfo infoTitle="Nr mieszkania:" infoContent={`${info.userData?.flatNumber}`} />
        <ProfileInfo infoTitle="Miasto:" infoContent={info.userData?.city} />
        <ProfileInfo infoTitle="Kod pocztowy:" infoContent={info.userData?.zipCode} />
        <ProfileInfo infoTitle="Nr telefonu:" infoContent={info.userData?.phoneNumber} />
      </p>
    </article>
  );
};
export default UserAdditionalInfo;
