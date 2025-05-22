import { Component } from '@angular/core';
import { UsersComponent } from './users/feature/users.component';

@Component({
  standalone: true,
  imports: [UsersComponent],
  selector: 'flexi-project-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'flexi-project';
}
