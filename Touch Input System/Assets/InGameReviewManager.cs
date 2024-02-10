using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Play.Review;

public class InGameReviewManager : MonoBehaviour
{
    private ReviewManager _reviewManager;

    public ReviewManager ReviewManager { get { return _reviewManager; } }


    public void StartReviewProcess()
    {
        StartCoroutine(SetGoogleInAppReview());
    }
    IEnumerator SetGoogleInAppReview()
    {
        var requestFlowOperation = ReviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        var _playReviewInfo = requestFlowOperation.GetResult();
        StartCoroutine(AskPlayerForReview(_playReviewInfo));
    }

    IEnumerator AskPlayerForReview(PlayReviewInfo info)
    {
        var launchFlowOperation = _reviewManager.LaunchReviewFlow(info);
        yield return launchFlowOperation;
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        // The flow has finished. The API does not indicate whether the user
        // reviewed or not, or even whether the review dialog was shown. Thus, no
        // matter the result, we continue our app flow.
    }
}
