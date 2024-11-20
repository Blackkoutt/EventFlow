import magnifying_glass from "../../assets/magnifying_glass.png";
const TopPageSearchInput = () => {
  return (
    <>
      <div className="relative inline-flex items-center">
        <input
          type="text"
          placeholder="Szukaj..."
          className=" rounded-lg shadow-lg text-sm py-1 px-1 w-44"
        />
        <button className="absolute top-[5px] right-2 bg-transparent px-0 py-0">
          <img src={magnifying_glass} alt="Ikona wyszukiwania" className="w-4 h-4" />
        </button>
      </div>
    </>
  );
};
export default TopPageSearchInput;
