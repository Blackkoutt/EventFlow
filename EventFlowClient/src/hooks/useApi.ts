import { useState, useCallback } from "react";
import ApiClient from "../services/api/ApiClientService";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import { HTTPMethod } from "../helpers/enums/HTTPMethodEnum";

interface RequestParams<TEntity> {
  httpMethod: HTTPMethod;
  id?: number;
  body?: TEntity;
  queryParams?: Record<string, any>;
}

interface GETRequestParams {
  id?: number;
  queryParams?: Record<string, any>;
}
interface POSTRequestParams<TEntity> {
  body: TEntity;
}
interface PUTRequestParams<TEntity> {
  id: number;
  body: TEntity;
}
interface DELETERequestParams {
  id: number;
}

function useApi<TEntity>(endpoint: ApiEndpoint) {
  const [data, setData] = useState<TEntity[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const request = useCallback(
    async ({ httpMethod, id, body, queryParams }: RequestParams<TEntity>) => {
      setLoading(true);
      setError(null);
      try {
        let response: TEntity[];
        switch (httpMethod) {
          case HTTPMethod.GET:
            response = await ApiClient.Get<TEntity[]>(endpoint, queryParams);
            console.log(
              `${HTTPMethod[httpMethod]} ${ApiEndpoint[endpoint]}${id ? `/${id}` : ""}:`,
              response
            );
            setData(response);
            break;
        }
      } catch (error) {
        if (error instanceof Error) {
          setError(error.message);
        } else {
          setError("Something went wrong");
        }
        console.log("Error: ", error);
      } finally {
        setLoading(false);
      }
    },
    [endpoint]
  );

  const get = ({ id, queryParams }: GETRequestParams): Promise<void> =>
    request({ httpMethod: HTTPMethod.GET, id: id, queryParams: queryParams });

  const post = ({ body }: POSTRequestParams<TEntity>): Promise<void> =>
    request({ httpMethod: HTTPMethod.POST, body: body });

  const put = ({ id, body }: PUTRequestParams<TEntity>): Promise<void> =>
    request({ httpMethod: HTTPMethod.PUT, id: id, body: body });

  const del = ({ id }: DELETERequestParams): Promise<void> =>
    request({ httpMethod: HTTPMethod.DELETE, id: id });

  return { data, error, loading, get, post, put, del };
}

export default useApi;
