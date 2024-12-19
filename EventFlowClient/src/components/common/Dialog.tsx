import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { forwardRef, useRef } from "react";
import { faCircleXmark } from "@fortawesome/free-solid-svg-icons";

interface DialogProps {
  children?: React.ReactNode;
}

const Dialog = forwardRef<HTMLDialogElement, DialogProps>(({ children }: DialogProps, ref) => {
  const dialogRef = useRef<HTMLDialogElement | null>(null);
  return (
    <dialog
      ref={(node) => {
        dialogRef.current = node;
        if (typeof ref === "function") {
          ref(node);
        } else if (ref) {
          ref.current = node;
        }
      }}
      className="rounded-xl p-7 relative overflow-visible"
    >
      <FontAwesomeIcon
        icon={faCircleXmark}
        className="absolute -right-3 -top-3 hover:cursor-pointer hover:opacity-95 bg-white rounded-full"
        style={{
          width: "35px",
          height: "35px",
        }}
        onClick={() => dialogRef.current?.close()}
      />
      {children}
    </dialog>
  );
});
export default Dialog;
