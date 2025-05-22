import { inject, Injectable, resource, signal } from '@angular/core';
import { rxResource, toSignal } from '@angular/core/rxjs-interop';
import { FlexiProjectApi } from '@flexi-project/shared/data-access';
import { Guid } from '@microsoft/kiota-abstractions';
import {
  CreateUserDto,
  UpdateUserDto,
} from 'libs/web/shared/data-access/src/lib/api-client/generated/flexi-project-api-client/models';
import { TableLazyLoadEvent } from 'primeng/table';
import { from, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  readonly #flexiProjectApi = inject(FlexiProjectApi);

  filter = signal<TableLazyLoadEvent>({});
  users = rxResource({
    request: this.filter,
    loader: ({ request }) => {
      const pageSize = request.rows ?? 0;
      const page = request.first ? Math.floor(request.first / pageSize) : 0;

      return from(
        this.#flexiProjectApi.client.users.get({
          queryParameters: { pageSize, page },
        })
      );
    },
  });

  userId$ = new Subject<Guid>();
  #userId = toSignal(this.userId$);
  userDetails = resource({
    request: this.#userId,
    loader: ({ request }) =>
      this.#flexiProjectApi.client.users.byUserId(request).get(),
  });

  addUser$ = new Subject<CreateUserDto | null>();
  #addUser = toSignal(this.addUser$);
  addedUser = resource({
    request: this.#addUser,
    loader: ({ request }) => this.#flexiProjectApi.client.users.post(request!),
  });

  updateUser$ = new Subject<{ userId: Guid; user: UpdateUserDto }>();
  #updateUser = toSignal(this.updateUser$);
  updatedUser = resource({
    request: this.#updateUser,
    loader: ({ request }) =>
      this.#flexiProjectApi.client.users
        .byUserId(request?.userId ?? '')
        .put(request?.user ?? {}),
  });

  deleteUser$ = new Subject<Guid | null>();
  #deleteUser = toSignal(this.deleteUser$);
  deletedUser = resource({
    request: this.#deleteUser,
    loader: ({ request }) =>
      this.#flexiProjectApi.client.users.byUserId(request ?? '').delete(),
  });
}
