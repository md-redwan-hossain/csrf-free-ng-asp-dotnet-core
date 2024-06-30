import {Component, inject} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {HttpClient} from "@angular/common/http";
import {environment} from "../environments/environment";
import {concatMap, of, switchMap, tap} from "rxjs";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  private readonly http = inject(HttpClient);

  login() {
    const csrfToken = localStorage.getItem('csrfToken');

    (csrfToken ? of(true) : this.fetchCsrfToken())
      .pipe(
        concatMap(() => {
          return this.http.post(environment.LOGIN, null, {withCredentials: true});
        }))
      .subscribe();
  }

  private fetchCsrfToken() {
    return this.http.post(environment.LoadCsrfToken, null, {withCredentials: true})
  }

}
