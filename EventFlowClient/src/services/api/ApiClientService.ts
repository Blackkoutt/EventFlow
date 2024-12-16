import { baseUrl } from "../../config/environment/Environment.ts";
import axios, { AxiosInstance, AxiosResponse } from "axios";
import { ApiUrlConfig } from "../../config/ApiUrlConfig.ts";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum.ts";

export const api: AxiosInstance = axios.create({
  baseURL: baseUrl,
  withCredentials: true,
});

async function Get<TEntity>(endpoint: ApiEndpoint, queryParams?: Record<string, any>) {
  try {
    const { url } = ApiUrlConfig[endpoint];

    const queryString = queryParams ? `?${new URLSearchParams(queryParams).toString()}` : "";

    const response = await api.get<TEntity>(url + queryString, { withCredentials: true });
    const code = response.status;
    const data = response.data;
    return [data, code];
  } catch (error) {
    throw error;
  }
}

async function Post<TEntity, TPostEntity>(endpoint: ApiEndpoint, body: TPostEntity) {
  try {
    const { url } = ApiUrlConfig[endpoint];

    const response = await api.post<TEntity>(url, body);
    const code = response.status;
    const data = response.data;
    return [data, code];
  } catch (error) {
    throw error;
  }
}

async function Put<TEntity, TPutEntity>(endpoint: ApiEndpoint, id: number, body: TPutEntity) {
  try {
    const { url } = ApiUrlConfig[endpoint];
    const fullUrl = `${url}/${id}`;

    const response = await api.put<TEntity>(fullUrl, body);
    const code = response.status;
    const data = response.data;
    return [data, code];
  } catch (error) {
    throw error;
  }
}

async function Patch<TEntity, TPatchEntity>(
  endpoint: ApiEndpoint,
  body?: TPatchEntity,
  id?: number
) {
  try {
    const { url } = ApiUrlConfig[endpoint];
    let fullUrl: string;
    if (id !== undefined) fullUrl = `${url}/${id}`;
    else fullUrl = `${url}`;

    const response = await api.patch<TEntity>(fullUrl, body);

    const code = response.status;
    const data = response.data;
    return [data, code];
  } catch (error) {
    throw error;
  }
}

async function Delete<TEntity>(endpoint: ApiEndpoint, id: number) {
  try {
    const { url } = ApiUrlConfig[endpoint];
    const fullUrl = `${url}/${id}`;

    const response = await api.delete<TEntity>(fullUrl);
    const code = response.status;
    const data = response.data;
    return [data, code];
  } catch (error) {
    throw error;
  }
}

const GetPhotoEndpoint = (photoEndpoint?: string): string => {
  return photoEndpoint ? `${baseUrl}${photoEndpoint}` : "";
};

const ApiMethod = {
  Get,
  GetPhotoEndpoint,
  Post,
  Put,
  Patch,
  Delete,
};

export default ApiMethod;
