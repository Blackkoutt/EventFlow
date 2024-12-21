interface ProfileInfoProps {
  infoTitle: string;
  infoContent?: string;
}

const ProfileInfo = ({ infoTitle, infoContent }: ProfileInfoProps) => {
  const displayCondition =
    infoContent !== undefined &&
    infoContent !== null &&
    infoContent !== "null" &&
    infoTitle !== "houseNumber" &&
    infoContent !== "0";

  return (
    <div className="flex flex-row justify-start items-start gap-4">
      <p className="w-[150px] font-bold text-[18px] text-textPrimary">{infoTitle}</p>
      <p className="text-[18px] text-textPrimary">
        {displayCondition ? infoContent : "Brak danych"}
      </p>
    </div>
  );
};

export default ProfileInfo;
