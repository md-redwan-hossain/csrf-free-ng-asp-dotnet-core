import {ApplicationConfig, provideZoneChangeDetection} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {csrfInterceptor} from "./csrf.interceptor";

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([csrfInterceptor])),
    provideZoneChangeDetection({eventCoalescing: true}),
    provideRouter(routes)]
};
