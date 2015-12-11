import React from 'react';
import {FourSquareApp} from './FourSquareApp';

console.log('Initializing FourSquare search app.');

React.render(
<FourSquareApp getDataURL={$('#FourSquareSearchModule').attr('data-get-data-url')}
               preSelectedPlaces={JSON.parse($('#FourSquareSearchModule').attr('data-preselected-places'))}
               defaultPlace={$('#FourSquareSearchModule').attr('data-default-place')}/>,
document.getElementById('FourSquareSearchModule'));