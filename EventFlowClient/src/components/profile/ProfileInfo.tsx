interface ProfileInfoProps {
  infoTitle: string;
  infoContent?: string;
}

const ProfileInfo = ({ infoTitle, infoContent }: ProfileInfoProps) => {
  return (
    <div className="flex flex-row justify-start items-start gap-4">
      <p className="w-[150px] font-bold text-[18px]">{infoTitle}</p>
      <p className="text-[18px]">{infoContent ? infoContent : "Brak danych"}</p>
    </div>
  );
};

export default ProfileInfo;
