import { useState } from "react";

interface ToggleButton {
  width?: number;
  height?: number;
  toggleWidth?: number;
  toggleHeight?: number;
  toggleLeft?: number;
}
const ToggleButton = ({
  width = 50,
  height = 28,
  toggleWidth = 18,
  toggleHeight = 18,
  toggleLeft = 6,
}: ToggleButton) => {
  const [toggled, setToggled] = useState(false);
  const [focused, setFocused] = useState(false);
  return (
    <>
      <button
        className={`rounded-full 
         border-none relative cursor-pointer shadow-sm ${
           toggled ? "bg-primaryPurple" : "bg-[#808080] focus:outline-none"
         }`}
        onFocus={() => setFocused(true)}
        onBlur={() => setFocused(false)}
        style={{
          outline: focused ? "none" : "",
          width: `${width}px`,
          height: `${height}px`,
        }}
        onClick={() => setToggled(!toggled)}
      >
        <div
          className={`bg-white rounded-full
            transform transition-all duration-150 ease-in-out absolute
            left-1 top-[50%] translate-x-0 translate-y-[-50%]
            ${toggled ? "" : ""}`}
          style={{
            width: `${toggleWidth}px`,
            height: `${toggleHeight}px`,
            left: toggled ? `calc(${width}px - ${toggleWidth + toggleLeft}px)` : `${toggleLeft}px`,
          }}
        ></div>
      </button>
    </>
  );
};
export default ToggleButton;
