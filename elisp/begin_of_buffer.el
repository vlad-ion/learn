(defun begin_of_buffer ()
  (interactive)
  (push-mark)
  (goto-char (point-min)))

(defun do_add (a)
  ((lambda (x y)
    (+ x y)
  ) a 2)
)

((do_add 2)

(* 3.68 1500)

(/ 2025 3.68)

(defun app-to-buf (buffer start end)
  (interactive
  (list (read-buffer "Append to buffer: " (other-buffer
					   (current-buffer) t))
	(region-beginning)
	(region-end)))
  (let ((oldbuf (current-buffer)))
    (save-excursion
      (let* ((append-to (get-buffer-create buffer))
	     (windows (get-buffer-window-list append-to t t))
	     point)
	(set-buffer append-to)
	(setq point (point))
	(barf-if-buffer-read-only)
	(insert-buffer-substring oldbuf start end)
	(dolist (window windows)
	  (when (= (window-point window) point)
	    (set-window-point window (point))))))))

;quine
((lambda (x) (list x (list (quote(quote)) x)))
 (quote(lambda (x) (list x (list (quote(quote)) x)))))

(/ (+ 8 1.99 13.77 6.95) 2)

(* 3.85 360)