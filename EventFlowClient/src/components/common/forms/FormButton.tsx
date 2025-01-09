import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface FormButtonProps {
  isSubmitting: boolean;
  text: string;
  background?: string;
  width?: number;
  rounded?: number;
  iconSize?: number;
  height?: number;
  icon?: IconDefinition;
  py?: number;
  form?: string;
}

const FormButton = ({
  isSubmitting,
  text,
  width,
  rounded,
  icon,
  height,
  iconSize,
  form,
  background = "#7B2CBF",
  py = 20,
}: FormButtonProps) => {
  return (
    <button
      disabled={isSubmitting}
      form={form}
      style={{
        background: background,
        borderRadius: `${rounded ? `${rounded}px` : "6px"}`,
        width: `${width ? `${width}px` : "100%"}`,
        paddingTop: `${py}px`,
        height: `${height}px`,
        paddingBottom: `${py}px`,
      }}
      className="text-white flex flex-row justify-center items-center gap-3"
      type="submit"
    >
      {icon && <FontAwesomeIcon icon={icon} style={{ fontSize: `${iconSize}px` }} />}
      <div> {isSubmitting ? "Ładowanie..." : text}</div>
    </button>
  );
};

export default FormButton;
