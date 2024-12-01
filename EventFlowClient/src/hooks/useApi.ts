import { useState, useCallback } from "react";
import ApiClient, { api } from "../services/api/ApiClientService";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import { HTTPMethod } from "../helpers/enums/HTTPMethodEnum";
import { AxiosError } from "axios";
import { APIError } from "../models/error/APIError";
import { toast } from "react-toastify";

interface RequestParams<TPostEntity, TPutEntity, TPatchEntity> {
  httpMethod: HTTPMethod;
  id?: number;
  body?: TPostEntity | TPutEntity | TPatchEntity;
  queryParams?: Record<string, any>;
}

interface GETRequestParams {
  id?: number;
  queryParams?: Record<string, any>;
}
interface POSTRequestParams<TPostEntity> {
  body: TPostEntity;
}
interface PUTRequestParams<TPutEntity> {
  id: number;
  body: TPutEntity;
}
interface PATCHRequestParams<TPatchEntity> {
  id?: number;
  body?: TPatchEntity;
}
interface DELETERequestParams {
  id: number;
}

function useApi<TEntity, TPostEntity = undefined, TPutEntity = undefined, TPatchEntity = undefined>(
  endpoint: ApiEndpoint
) {
  const [data, setData] = useState<TEntity[]>([]);
  const [statusCode, setStatusCode] = useState<number | null>(null);
  const [error, setError] = useState<APIError | null>(null);
  const [loading, setLoading] = useState(false);

  const request = useCallback(
    async ({
      httpMethod,
      id,
      body,
      queryParams,
    }: RequestParams<TPostEntity, TPutEntity, TPatchEntity>) => {
      setLoading(true);
      setError(null);
      try {
        switch (httpMethod) {
          case HTTPMethod.GET:
            const [getData, getCode] = await ApiClient.Get<TEntity[]>(endpoint, queryParams);
            let dataArray: TEntity[] = getData as TEntity[];
            if (!Array.isArray(dataArray)) dataArray = [getData as TEntity];
            setData(dataArray);
            setStatusCode(getCode as number);
            break;

          case HTTPMethod.POST:
            if (typeof body === undefined || body === undefined)
              throw Error("POST Error: body is undefined");
            const [postData, postCode] = await ApiClient.Post<TEntity, TPostEntity>(
              endpoint,
              body as TPostEntity
            );
            /* setData((prev) => {
              if (Array.isArray(prev)) {
                return [...prev, postData as TEntity];
              } else {
                return postData as TEntity;
              }
            });*/
            setData((prev) => [...prev, postData as TEntity]);
            setStatusCode(postCode as number);
            break;

          case HTTPMethod.PUT:
            if (id === undefined) throw Error("PUT Error: id is undefined");
            if (typeof body === undefined || body === undefined)
              throw Error("PUT Error: body is undefined");
            const [putData, putCode] = await ApiClient.Put<TEntity, TPutEntity>(
              endpoint,
              id as number,
              body as TPutEntity
            );
            /* setData((prev) =>
              prev.map((item) => (item.id === id ? { ...item, ...putResponse } : item))
            );*/
            setStatusCode(putCode as number);
            break;

          case HTTPMethod.PATCH:
            console.log("heloov2");
            console.log("endpoint", endpoint);
            const [patchData, patchCode] = await ApiClient.Patch<TEntity, TPatchEntity>(
              endpoint,
              body as TPatchEntity | undefined,
              id
            );
            console.log("patchCode", patchCode);
            /* setData((prev) =>
                prev.map((item) => (item.id === id ? { ...item, ...putResponse } : item))
              );*/
            setStatusCode(patchCode as number);
            break;

          case HTTPMethod.DELETE:
            if (id === undefined) throw Error("DELETE Error: id is undefined");
            const [deleteData, deleteCode] = await ApiClient.Delete(endpoint, id);
            //setData((prev) => prev.filter((item) => item.id !== id));
            setStatusCode(deleteCode as number);
            break;
          default:
            throw new Error(`Unsupported HTTP method: ${httpMethod}`);
        }
      } catch (catchedError) {
        if (catchedError instanceof AxiosError) {
          const apiError: APIError = {
            code: catchedError.response?.data.Code || catchedError.response?.data.code,
            title: catchedError.response?.data.Title || catchedError.response?.data.title,
            type: catchedError.response?.data.Type || catchedError.response?.data.type,
            details: catchedError.response?.data.Details || catchedError.response?.data.details,
          };

          console.log("API Error: ", apiError);
          setError(apiError);
          setStatusCode(apiError.code);
          if (apiError.details.errors.length > 0) {
            apiError.details.errors.forEach((error) => {
              toast.error(error);
            });
          }
        }
      } finally {
        setLoading(false);
      }
    },
    [endpoint]
  );

  const get = useCallback(
    ({ id, queryParams }: GETRequestParams): Promise<void> =>
      request({ httpMethod: HTTPMethod.GET, id, queryParams }),
    [request]
  );

  const post = useCallback(
    ({ body }: POSTRequestParams<TPostEntity>): Promise<void> =>
      request({ httpMethod: HTTPMethod.POST, body }),
    [request]
  );

  const put = useCallback(
    ({ id, body }: PUTRequestParams<TPutEntity>): Promise<void> =>
      request({ httpMethod: HTTPMethod.PUT, id, body }),
    [request]
  );

  const patch = useCallback(
    ({ id, body }: PATCHRequestParams<TPatchEntity>): Promise<void> =>
      request({ httpMethod: HTTPMethod.PATCH, id, body }),
    [request]
  );

  const del = useCallback(
    ({ id }: DELETERequestParams): Promise<void> => request({ httpMethod: HTTPMethod.DELETE, id }),
    [request]
  );

  return { data, error, statusCode, loading, get, post, put, patch, del };
}

export default useApi;

// function useApi<TEntity, TPostEntity=undefined, TPutEntity=undefined>(endpoint: ApiEndpoint) {
//   const [data, setData] = useState<TEntity[]>([]);
//   const [error, setError] = useState<string | null>(null);
//   const [loading, setLoading] = useState(false);

//   const request = useCallback(
//     async ({ httpMethod, id, body, queryParams }: RequestParams<TPostEntity, TPutEntity>) => {
//       setLoading(true);
//       setError(null);
//       try {
//         let response: TEntity[] = [];
//         switch (httpMethod) {
//           case HTTPMethod.GET:
//             response = await ApiClient.Get<TEntity[]>(endpoint, queryParams);
//             setData(response);
//             break;
//           case HTTPMethod.POST:
//             if()
//         }
//         console.log(
//           `${HTTPMethod[httpMethod]} ${ApiEndpoint[endpoint]}${id ? `/${id}` : ""}:`,
//           response
//         );
//       } catch (error) {
//         if (error instanceof Error) {
//           setError(`${HTTPMethod[httpMethod]} ${ApiEndpoint[endpoint]}${id ? `/${id}` : ""}: ${error.message}`);
//         } else {
//           setError("Something went wrong");
//         }
//         console.log("Error: ", error);
//       } finally {
//         setLoading(false);
//       }
//     },
//     [endpoint]
//   );

//   const get = ({ id, queryParams }: GETRequestParams): Promise<void> =>
//     request({ httpMethod: HTTPMethod.GET, id: id, queryParams: queryParams });

//   const post = <TPostEntity>({ body }: POSTRequestParams<TPostEntity>): Promise<void> =>
//     request({ httpMethod: HTTPMethod.POST, body: body });

//   const put = ({ id, body }: PUTRequestParams<TEntity>): Promise<void> =>
//     request({ httpMethod: HTTPMethod.PUT, id: id, body: body });

//   const del = ({ id }: DELETERequestParams): Promise<void> =>
//     request({ httpMethod: HTTPMethod.DELETE, id: id });

//   return { data, error, loading, get, post, put, del };
// }

// export default useApi;