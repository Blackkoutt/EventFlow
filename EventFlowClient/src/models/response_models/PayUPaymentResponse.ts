export type PayUPaymentResponseStatusCode = {
  statusCode: string;
};

export type PayUPaymentResponse = {
  status: PayUPaymentResponseStatusCode;
  redirectUri: string;
  orderId: string;
};
