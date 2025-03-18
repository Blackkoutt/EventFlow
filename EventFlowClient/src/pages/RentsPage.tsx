import { useEffect, useState } from "react";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { Hall, HallType } from "../models/response_models";
import HallCard from "../components/hallrentspage/HallCard";

interface NameValue {
  name: string;
  value: number | null;
}

function RentsPage() {
  const { data: hallTypes, get: getHallTypes } = useApi<HallType>(ApiEndpoint.HallType);
  const { data: halls, get: getHalls } = useApi<Hall>(ApiEndpoint.Hall);
  const [selectedType, setSelectedType] = useState<NameValue>({
    name: "Wszystkie",
    value: null,
  });

  useEffect(() => {
    getHallTypes({ id: undefined, queryParams: undefined });
    getHalls({ id: undefined, queryParams: undefined });
  }, []);

  console.log(halls);
  useEffect(() => {
    let queryParams;
    if (selectedType.value != null) {
      queryParams = {
        hallTypeId: selectedType.value,
      };
    } else {
      queryParams = undefined;
    }

    getHalls({ id: undefined, queryParams: queryParams });
  }, [selectedType]);

  return (
    <div className="w-full py-10">
      <div className="flex flex-col gap-6">
        <h1>Wynajmy</h1>
        <div className="flex justify-center items-center flex-wrap min-w-[150px] gap-4">
          <p className="font-semibold text-[18px]">Kategorie:</p>
          <button
            className={`px-4 py-2 rounded-full ${
              selectedType.name == "Wszystkie"
                ? "text-white bg-primaryPurple"
                : "bg-gray-200 text-gray-600"
            } `}
            style={{ fontSize: 18 }}
            onClick={() =>
              setSelectedType({
                name: "Wszystkie",
                value: null,
              })
            }
          >
            Wszystkie
          </button>
          {hallTypes
            .filter((type) => type.name !== "Sala ogólna")
            .map((type) => (
              <button
                key={type.id}
                className={`px-4 py-2 rounded-full min-w-[150px] ${
                  selectedType.name == type.name
                    ? "text-white bg-primaryPurple"
                    : "bg-gray-200 text-gray-600"
                }`}
                onClick={() =>
                  setSelectedType({
                    name: type.name,
                    value: type.id,
                  })
                }
                style={{ fontSize: 18 }}
              >
                {type.name}
              </button>
            ))}
        </div>
      </div>
      <div className="py-6">
        {halls && (
          <div className="grid 3xl:grid-cols-3 xl:grid-cols-2 3xl:gap-4 xl:gap-10">
            {halls.map((item) => (
              <HallCard key={item.id} item={item} />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
export default RentsPage;
