import { useEffect } from "react";
import { useLocation } from "react-router-dom";

const ScrollToTop = () => {
  const location = useLocation();

  useEffect(() => {
    window.scrollTo(0, 0); // Przewijanie na początek strony
  }, [location]);

  return null; // Ten komponent nie renderuje nic
};

export default ScrollToTop;
