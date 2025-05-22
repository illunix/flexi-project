import { inject, Injectable } from '@angular/core';
import { APP_CONFIG } from '@flexi-project/shared/app-config';
import { AnonymousAuthenticationProvider } from '@microsoft/kiota-abstractions';
import {
  FetchRequestAdapter,
  KiotaClientFactory,
} from '@microsoft/kiota-http-fetchlibrary';
import {
  createFlexiProjectApiClientGenerated,
  FlexiProjectApiClientGenerated,
} from './generated/flexi-project-api-client/flexiProjectApiClientGenerated';

@Injectable({ providedIn: 'root' })
export class FlexiProjectApi {
  private readonly appConfig = inject(APP_CONFIG);

  private adapter = new FetchRequestAdapter(
    new AnonymousAuthenticationProvider(),
    undefined,
    undefined,
    KiotaClientFactory.create()
  );

  public client: FlexiProjectApiClientGenerated;

  public constructor() {
    this.adapter.baseUrl = this.appConfig.baseURL;
    this.client = createFlexiProjectApiClientGenerated(this.adapter);
  }
}
