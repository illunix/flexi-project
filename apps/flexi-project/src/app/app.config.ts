import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';
import { provideAppConfig } from '@flexi-project/shared/app-config';
import Material from '@primeng/themes/material';
import { providePrimeNG } from 'primeng/config';
import { environment } from '../environments/environment.prod';
import { appRoutes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideClientHydration(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(appRoutes),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Material,
        options: {
          darkModeSelector: false || 'none',
        },
      },
    }),
    provideAppConfig(environment),
    provideHttpClient(),
  ],
};
