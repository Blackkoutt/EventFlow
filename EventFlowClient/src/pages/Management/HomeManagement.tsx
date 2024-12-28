import management1 from "../../assets/management1.png";

const HomeManagement = () => {
  return (
    <div className="flex flex-col self-center justify-center items-center gap-10 w-full pt-7">
      <h2 className="tracking-wide">
        <span className="text-[#2F2F2F] text-4xl font-semibold">Witaj w panelu zarządzania </span>
        <span className="text-primaryPurple font-extrabold text-5xl">EventFlow!</span>
      </h2>
      <img
        src={management1}
        alt="Obrazek panelu zarządzania"
        className="object-contain w-[460px] h-[460px]"
      />
    </div>
  );
};
export default HomeManagement;
