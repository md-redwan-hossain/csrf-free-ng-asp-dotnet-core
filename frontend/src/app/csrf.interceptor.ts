import {HttpErrorResponse, HttpInterceptorFn, HttpResponse} from '@angular/common/http';
import {catchError, of, tap} from "rxjs";

export const csrfInterceptor: HttpInterceptorFn = (req, next) => {
  const csrfToken = localStorage.getItem('csrfToken');

  const clonedReq = req.clone({
    withCredentials: true,
    setHeaders: csrfToken ? {'X-XSRF-TOKEN': csrfToken} : {}
  });

  return next(clonedReq).pipe(
    catchError((error: any) => {
      if (error instanceof HttpErrorResponse) {
        const csrfToken = error.headers.get('X-XSRF-TOKEN');
        if (csrfToken) {
          localStorage.setItem('csrfToken', csrfToken);
        }
      }

      return of(error);
    }),

    tap((event) => {
      if (event instanceof HttpResponse) {
        const csrfToken = event.headers.get('X-XSRF-TOKEN');
        if (csrfToken) {
          localStorage.setItem('csrfToken', csrfToken);
        }
      }
    }));
};

