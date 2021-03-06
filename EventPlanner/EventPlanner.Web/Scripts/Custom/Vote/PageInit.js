﻿import React from 'react';
import {VotingApp} from './VotingApp';

var APP = APP || {};
APP.datesAppId = 'DatesVotingApp';
APP.placesAppId = 'PlacesVotingApp';
APP.initializeVotingComponent = (elementId) => {
  var selector = '#'+elementId;
  React.render(<
        VotingApp loadingImgUrl={$(selector).attr('data-loading-img-url')}
                  eventId={$(selector).attr('data-event-id')}
                  submitVotesUrl={$(selector).attr('data-submit-vote-url')}
                  getInitialDataUrl={$(selector).attr('data-get-initial-data-url')}/>, 
        document.getElementById(elementId));
};

$(function() {
  console.log('Initializing Dates voting app.');
  console.log(VotingApp);
  APP.initializeVotingComponent(APP.datesAppId);

  console.log('Initializing Places voting app.');
  APP.initializeVotingComponent(APP.placesAppId);
});
