import { useEffect, useState } from "react";
import viteLogo from "/vite.svg";
import "./App.css";
import ApiClient from "./services/api/ApiClientService";
import { ApiEndpoint } from "./helpers/enums/ApiEndpointEnum";
import { EventEntity } from "./models/response_models";

function App() {
  const [count, setCount] = useState(0);
  const [events, setEvents] = useState<EventEntity[]>([]);

  useEffect(() => {
    ApiClient.Get<EventEntity[]>(ApiEndpoint.Event)
      .then((data) => {
        console.log("Dane:", data);
        setEvents(data);
      })
      .catch((error) => {
        console.error("Błąd podczas pobierania danych:", error);
      });
  }, []);

  return (
    <>
      <div className="flex justify-center">
        {events.map((el) => (
          <p>{el.name}</p>
        ))}
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo min-w-24 min-h-36 object-cover" alt="Vite logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>count is {count}</button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">Click on the Vite and React logos to learn more</p>
    </>
  );
}

export default App;
