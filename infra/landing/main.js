/* =============================================================================
 * File:           infra/landing/main.js
 * Author:         USDTG GROUP TECHNOLOGY LLC
 * Developer:      Irfan Gedik
 * Created Date:   2026-05-03
 * Last Update:    2026-05-03
 * Version:        0.1.0
 *
 * Description:
 *   Landing-page-only progressive enhancement: the email signup form posts
 *   to the nyrduel gateway's /signup endpoint and falls back to a mailto:
 *   link if the network is unreachable. No frameworks; runs without build.
 *
 * License:
 *   Proprietary. All rights reserved. See LICENSE in the repository root.
 * ============================================================================= */

(function () {
  "use strict";

  var SIGNUP_ENDPOINT = "https://nyrduel.com/signup";
  var FALLBACK_MAIL = "hello@nyrvexa.com";

  function $(sel) { return document.querySelector(sel); }

  function setStatus(el, text, kind) {
    el.textContent = text;
    el.className = "signup-status" + (kind ? " " + kind : "");
  }

  function isValidEmail(s) {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(s);
  }

  function init() {
    var form = $("#signup-form");
    var status = $("#signup-status");
    if (!form || !status) return;

    form.addEventListener("submit", function (ev) {
      ev.preventDefault();
      var input = form.querySelector('input[name="email"]');
      var email = (input && input.value || "").trim();
      if (!isValidEmail(email)) {
        setStatus(status, "Looks like that's not a valid email — try again.", "err");
        return;
      }

      setStatus(status, "Sending…", "");

      fetch(SIGNUP_ENDPOINT, {
        method: "POST",
        headers: { "content-type": "application/json", accept: "application/json" },
        body: JSON.stringify({ v: 1, email: email, source: "nyrvexa.com" })
      })
        .then(function (res) {
          if (res.ok) return res.json();
          throw new Error("HTTP " + res.status);
        })
        .then(function () {
          setStatus(status, "✓ You're on the list. We'll send one message at launch.", "ok");
          input.value = "";
        })
        .catch(function () {
          var subject = "Notify me when Nyrvexa launches";
          var body = "Email: " + email;
          var href =
            "mailto:" + FALLBACK_MAIL +
            "?subject=" + encodeURIComponent(subject) +
            "&body=" + encodeURIComponent(body);
          status.innerHTML =
            "Couldn't reach the signup service. " +
            '<a href="' + href + '">Email us instead</a>.';
          status.className = "signup-status err";
        });
    });
  }

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", init);
  } else {
    init();
  }
})();
