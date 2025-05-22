import {
  ChangeDetectionStrategy,
  Component,
  inject,
  ResourceStatus,
  signal,
} from '@angular/core';
import { Guid } from '@microsoft/kiota-abstractions';
import { UserDto } from 'libs/web/shared/data-access/src/lib/api-client/generated/flexi-project-api-client/models';
import { explicitEffect } from 'ngxtension/explicit-effect';
import { RepeatPipe } from 'ngxtension/repeat-pipe';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SkeletonModule } from 'primeng/skeleton';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { Toast } from 'primeng/toast';
import { FileConverter } from '../../shared/data-access/services/file-converter.service';
import { UsersService } from '../data-access/users.service';
import { AddEditUserDialogComponent } from '../ui/add-edit-user-dialog/add-edit-user-dialog.component';

@Component({
  standalone: true,
  imports: [
    TableModule,
    ButtonModule,
    SkeletonModule,
    AvatarModule,
    RepeatPipe,
    AddEditUserDialogComponent,
    Toast,
    ConfirmDialogModule,
  ],
  providers: [MessageService, ConfirmationService],
  selector: 'flexi-project-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UsersComponent {
  readonly usersService = inject(UsersService);
  readonly fileConverter = inject(FileConverter);
  readonly #confirmationService = inject(ConfirmationService);
  readonly #msgService = inject(MessageService);

  readonly showAddEditUserDialog = signal<boolean>(false);
  readonly selectedUserId = signal<Guid | undefined>(undefined);

  constructor() {
    explicitEffect(
      [
        this.usersService.addedUser.status,
        this.usersService.updatedUser.status,
      ],
      ([addedUserStatus, updatedUserStatus]) => {
        if (
          addedUserStatus === ResourceStatus.Resolved ||
          updatedUserStatus === ResourceStatus.Resolved
        ) {
          this.usersService.users.reload();
        }
      }
    );
    explicitEffect([this.usersService.deletedUser.status], ([deletedUser]) => {
      switch (deletedUser) {
        case ResourceStatus.Resolved:
          this.#msgService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Successfully deleted user',
            life: 3000,
          });
          this.usersService.users.reload();
          break;
        case ResourceStatus.Error:
          this.#msgService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong while deleting user',
            life: 3000,
          });
          break;
      }
    });
  }

  load(event: TableLazyLoadEvent) {
    const current = this.usersService.filter();

    const isSamePage =
      current.first === event.first &&
      current.rows === event.rows &&
      current.sortField === event.sortField &&
      current.sortOrder === event.sortOrder &&
      JSON.stringify(current.filters) === JSON.stringify(event.filters);

    if (!isSamePage) {
      this.usersService.filter.set(event);
    }
  }

  onToggleAddEditUserDialog(userId: Guid | undefined = undefined) {
    this.selectedUserId.set(userId);
    this.showAddEditUserDialog.set(!this.showAddEditUserDialog());
  }

  onDeleteUser(event: Event, userId: Guid) {
    this.#confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Are you sure you want delete this user?',
      header: 'Delete confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-text button-round-xs',
      rejectButtonStyleClass: 'p-button-text p-button-text button-round-xs',
      acceptIcon: 'none',
      rejectIcon: 'none',
      acceptLabel: 'Yes',
      rejectLabel: 'No',
      accept: () => this.usersService.deleteUser$.next(userId),
    });
  }

  getFirstLetter(str: string) {
    return str[0];
  }

  getAvatarUrl(user: UserDto): string {
    const file = this.fileConverter.fromBase64(
      user.avatar!,
      `${user.name}-avatar`
    );

    return URL.createObjectURL(file);
  }
}
