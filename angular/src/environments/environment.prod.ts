import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Tedu_Ecommance',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44325/',
    redirectUri: baseUrl,
    clientId: 'Tedu_Ecommance_App',
    responseType: 'code',
    scope: 'offline_access Tedu_Ecommance',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44358',
      rootNamespace: 'Tedu_Ecommance',
    },
  },
} as Environment;
