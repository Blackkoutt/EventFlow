import { faCartShopping } from "@fortawesome/free-solid-svg-icons";
import { EventPassType } from "../../models/response_models";
import Button, { ButtonStyle } from "../buttons/Button";

interface BuyEventPassTypeProps {
  item: EventPassType;
  onClick: () => void;
}

const BuyEventPassType = ({ item, onClick }: BuyEventPassTypeProps) => {
  return (
    <article className="flex flex-col gap-8 justify-start items-start shadow-xl px-16 py-10 rounded-lg border-[1px] border-[#efefef] bg-white">
      <h2 className="text-black">{item.name.toLocaleUpperCase()}</h2>
      <ul className="list-disc text-textPrimary ">
        <li>Długość trwania: {item.validityPeriodInMonths} mies</li>
        <li>Procent zniżki przy przedłużeniu: {item.renewalDiscountPercentage} %</li>
      </ul>
      <div className="flex flex-row justify-between items-center w-full">
        <p className="text-black font-bold text-[22px]">{item.price} PLN</p>
        <Button
          text="Kup karnet"
          height={45}
          icon={faCartShopping}
          width={150}
          fontSize={16}
          isFontSemibold={true}
          style={ButtonStyle.Primary}
          action={onClick}
        />
      </div>
    </article>
  );
};
export default BuyEventPassType;
