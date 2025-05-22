import { ValueProvider } from '@angular/core';
import { APP_CONFIG } from './app-config.token';

export interface AppConfig {
  production: boolean;
  baseURL: string;
}

export const provideAppConfig = (value: AppConfig): ValueProvider => ({
  provide: APP_CONFIG,
  useValue: value,
});
